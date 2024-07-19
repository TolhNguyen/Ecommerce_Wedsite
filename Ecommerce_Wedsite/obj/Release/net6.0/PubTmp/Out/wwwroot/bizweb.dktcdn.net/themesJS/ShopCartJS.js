// Ghi chú: cname là cart, exdays = 30 days, cvalue là giá trị

function setCart(cvalue) {
    const d = new Date();
    d.setTime(d.getTime() + (1 * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = "cart" + "=" + cvalue + ";" + expires + ";path=/";
}

function getCart() {
    let name = "cart=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function logCart() {
    var a = getCart();
    console.log(a);
}

function checkCart() {
    let username = getCookie("username");
    if (username != "") {
        alert("Welcome again " + username);
    } else {
        username = prompt("Please enter your name:", "");
        if (username != "" && username != null) {
            setCart("username", username, 365);
        }
    }
}



