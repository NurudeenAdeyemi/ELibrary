// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.

let Url = "https://localhost:44307/api/Category";
async function getCategories() {
    let Url = "https://localhost:44307/api/Category";
    /*let data = await fetch(Url).then((res) => { return res; }).then
        ((data) => { return data;});*/

    let response = await axios.get(Url);
    alert(response.data.data[0].name);
    document.getElementById("test1").innerHTML = response.data.data[0].name;
}


async function postCategories() {
    let category = {
        name:"politics"
    }

    await axios.post(Url, category, function () {
        console.log("done")
    }).catch((err) => console.log(err));
}

postCategories();
getCategories();