let users = [];
let products = [];
let orders = [];
let noncrud = [];
let userToUpdate = -1;
let productToUpdate = -1;
let orderToUpdate = -1;

getdataU();
getdataP();
getdataO();
NonCrud();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:39354/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("UserCreated", (user, message) => {
        getdataU();
    });

    connection.on("UserDeleted", (user, message) => {
        getdataU();
    });

    connection.on("UserUpdated", (user, message) => {
        getdataU();
    });


    connection.on("ProductCreated", (product, message) => {
        getdataP();
    });

    connection.on("ProductDeleted", (product, message) => {
        getdataP();
    });

    connection.on("ProductUpdated", (product, message) => {
        getdataP();
    });

    connection.on("OrderCreated", (product, message) => {
        getdataO();
    });

    connection.on("OrderDeleted", (product, message) => {
        getdataO();
    });

    connection.on("OrderUpdated", (order, message) => {
        getdataO();
    });


    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}




async function getdataU() {
    fetch('http://localhost:39354/user')
    .then(x => x.json())
    .then(y => {
        users = y;
        console.log(users);
        displayU();
    });
}

function displayU() {
    document.getElementById('resultareaU').innerHTML = "";
    users.forEach(t => {
        document.getElementById('resultareaU').innerHTML +=
            "<tr><td>" + t.userId + " </td><td>"
            + t.username + "</td><td>" +
        `<button type="button" onclick="removeU(${t.userId})">Delete</button>` +
        `<button type="button" onclick="showupdateU(${t.userId})">Update</button>`
            + "</td></tr>";


    });


    

}

function showupdateU(id) {

    document.getElementById('updateusername').value = users.find(t => t["userId"] == id)['username'];
    document.getElementById('updateformdiv').style.display = 'flex';
    userToUpdate = id;
}

function updateU() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('updateusername').value;
    fetch('http://localhost:39354/user', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { username: name, userId:userToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataU();
        })
        .catch((error) => { console.error('Error:', error); });


}

function removeU(id) {
   

        fetch('http://localhost:39354/user/' + id, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json', },
            body: null
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdataU();
                
                

                ;
                
            })
            .catch((error) => { console.error('Error:', error); });
    
}

function createU() {
    let name = document.getElementById('Username').value;
    fetch('http://localhost:39354/user', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { username: name })})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);            
            getdataU();
        })
        .catch((error) => { console.error('Error:', error); });
    

}




async function getdataP() {
    fetch('http://localhost:39354/product')
        .then(x => x.json())
        .then(y => {
            products = y;
            console.log(products);
            displayP();
        });
}

function displayP() {
    document.getElementById('resultareaP').innerHTML = "";
    products.forEach(t => {
        document.getElementById('resultareaP').innerHTML +=
            "<tr><td>" + t.productId + " </td><td>"
        + t.productName + "</td><td>" + t.price +"</td><td>"
        + `<button type="button" onclick="removeP(${t.productId})">Delete</button>` +
        `<button type="button" onclick="showupdateP(${t.productId})">Update</button>`
            + "</td></tr>";


    });

}

function removeP(id) {

   

        fetch('http://localhost:39354/product/' + id, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json', },
            body: null
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdataP();
                getdataO();
            })
            .catch((error) => { console.error('Error:', error); });
    
}

function createP() {
    let name = document.getElementById('ProductName').value;
    let Price = document.getElementById('Price').value;
    fetch('http://localhost:39354/product', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { productName: name,price : Price})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataP();
        })
        .catch((error) => { console.error('Error:', error); });


}

function showupdateP(id) {

    document.getElementById('updateproduct').value = products.find(t => t["productId"] == id)['productName'];
    document.getElementById('updatePrice').value = products.find(t => t["productId"] == id)['price'];
    document.getElementById('updateformdivp').style.display = 'flex';
    productToUpdate = id;
}

function updateP() {
    document.getElementById('updateformdivp').style.display = 'none';
    let name = document.getElementById('updateproduct').value;
    let Price = document.getElementById('updatePrice').value;
    fetch('http://localhost:39354/product', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { productName: name, productId: productToUpdate ,price :Price })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataP();
        })
        .catch((error) => { console.error('Error:', error); });


}




async function getdataO() {
    fetch('http://localhost:39354/order')
        .then(x => x.json())
        .then(y => {
            orders = y;
            console.log(orders);
            displayO();
        });
}

function displayO() {
    document.getElementById('resultareaO').innerHTML = "";
    orders.forEach(t => {
        document.getElementById('resultareaO').innerHTML +=
            "<tr><td>" + t.orderId + " </td><td>"
        + t.userId + "</td><td>" + t.productId + "</td><td>"       
            + `<button type="button" onclick="removeO(${t.orderId})">Delete</button>` +
            `<button type="button" onclick="showupdateO(${t.orderId})">Update</button>`
            + "</td></tr>";


    });

}

function removeO(id) {
    fetch('http://localhost:39354/order/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataO();
            
        })
        .catch((error) => { console.error('Error:', error); });

}

function createO() {
    let userID = document.getElementById('userID').value;
    let productID = document.getElementById('productID').value;
    fetch('http://localhost:39354/order', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { UserId: userID, ProductId: productID, orderId: orders.length+1  })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataO();
        })
        .catch((error) => { console.error('Error:', error); });


}

function showupdateO(id) {

    document.getElementById('updateuserid').value = orders.find(t => t["orderId"] == id)['userId'];
    document.getElementById('updateproductid').value = orders.find(t => t["orderId"] == id)['productId'];
    document.getElementById('updateformdivo').style.display = 'flex';
    orderToUpdate = id;
}

function updateO() {
    document.getElementById('updateformdivo').style.display = 'none';
    let userID = document.getElementById('updateuserid').value;
    let productID = document.getElementById('updateproductid').value;
    fetch('http://localhost:39354/order', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { UserId: userID, ProductId: productID, OrderId :orderToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdataO();
        })
        .catch((error) => { console.error('Error:', error); });



}

async function NonCrud() {
    fetch('http://localhost:39354/NonCrud/GetDatas')
        .then(x => x.json())
        .then(y => {
            noncrud = y;
            console.log(noncrud);
            displayN();
        });
}

function displayN() {
    document.getElementById('resultareaN').innerHTML = "";
    noncrud.forEach(t => {
        document.getElementById('resultareaN').innerHTML +=
            "<tr><td>" + t.id + " </td><td>"
            + t.users
            + "</td></tr>";



    });

}