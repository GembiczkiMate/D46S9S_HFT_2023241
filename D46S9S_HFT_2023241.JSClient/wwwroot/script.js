let users = [];
let products = [];
let orders = [];
getdataU();
getdataP();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:39354/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("UserCreated", (user, message) => {
        getdatau();
    });

    connection.on("UserDeleted", (user, message) => {
        getdatau();
    });

    connection.on("ProductCreated", (product, message) => {
        getdataP();
    });

    connection.on("ProductDeleted", (product, message) => {
        getdataP();
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
            `<button type="button" onclick="removeU(${t.userId})">Delete</button>`
            + "</td></tr>";


    });


    

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
           + `<button type="button" onclick="removeP(${t.productId})">Delete</button>`
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