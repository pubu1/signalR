// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(() => {
    LoadPostData();
    LoadUserData();
    LoadCatData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
    connection.start();
    connection.on("LoadPosts", function () {
        LoadPostData();
    })
    connection.on("LoadUsers", function () {
        LoadUserData();
    })
        connection.on("LoadCats", function () {
        LoadCatData();
    })
    LoadPostData();
    LoadCatData();
    LoadUserData();

    function LoadPostData() {
        var tr = '';
        $.ajax({
            url: '/Posts/GetPosts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr> 
                        <td>${v.user.fullname}</td>
                        <td>${v.createdDate}</td>
                        <td>${v.updatedDate}</td>
                        <td>${v.title}</td>
                        <td>${v.content}</td>
                        <td>${v.publicStatus}</td>
                        <td>${v.cat.categoryName}</td>
                        <td>
                            <a href='../Posts/Edit?id=${v.postID}'>Edit</a>
                            <a href='../Posts/Details?id=${v.postID}'>Details</a>
                            <a href='../Posts/Delete?id=${v.postID}'>Delete</a>
                        </td>
                    </tr>`
                })
                $("#tableBodyPost").html(tr);
            },
            error: (error) => {
                console.log(error)
            }

        });
    }
    function LoadUserData() {
        var tr = '';
        $.ajax({
            url: '/Users/GetUsers',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr> 
                        <td>${v.fullname}</td>
                        <td>${v.address}</td>
                        <td>${v.email}</td>
                        <td>${v.password}</td>
                        <td>
                            <a href='../Users/Edit?id=${v.userID}'>Edit</a>
                            <a href='../Users/Details?id=${v.userID}'>Details</a>
                            <a href='../Users/Delete?id=${v.userID}'>Delete</a>
                        </td>
                    </tr>`
                })
                $("#tableBodyUser").html(tr);
            },
            error: (error) => {
                console.log(error)
            }

        });
    }

    function LoadCatData() {
        var tr = '';
        $.ajax({
            url: '/Categories/GetCats',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr> 
                        <td>${v.categoryName}</td>
                        <td>${v.description}</td>
                        <td>
                            <a href='../Categories/Edit?id=${v.categoryID}'>Edit</a>
                            <a href='../Categories/Details?id=${v.categoryID}'>Details</a>
                            <a href='../Categories/Delete?id=${v.categoryID}'>Delete</a>
                        </td>
                    </tr>`
                })
                $("#tableBodyCat").html(tr);
            },
            error: (error) => {
                console.log(error)
            }

        });
    }
    
})




