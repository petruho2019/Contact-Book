
function validateContactForm() {
    let valid = true;

    // Очистка предыдущих сообщений
    document.querySelectorAll(".error-message").forEach(el => el.innerText = "");

    let name = document.getElementById("name").value;

    let error = findEmptyValue({ "name": name });

    if (error != null) {
        document.getElementById(error + "Error").innerText = error.replace(/^\w/, c => c.toUpperCase()) + " is required";
        valid = false;
        return valid;
    }

    return valid; // Если valid = false, форма не отправится
}

function validateRegisterForm() {
    let valid = true;

    // Очистка предыдущих сообщений
    document.querySelectorAll(".error-message").forEach(el => el.innerText = "");

    let username = document.getElementById("username").value;
    let age = document.getElementById("age").value;
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;

    let error = findEmptyValue({ "username": username, "age": age, "email": email, "password": password });

    if (error != null) {
        document.getElementById(error + "Error").innerText = error.replace(/^\w/, c => c.toUpperCase()) + " is required";
        valid = false;
        return valid;
    }

    if (username.length < 3) {
        document.getElementById("usernameError").innerText = "Username must be at least 3 characters long";
        valid = false;
        return valid;
    }

    if (age < 0) {
        document.getElementById("ageError").innerText = "Age has incorrect input";
        valid = false;
        return valid;
    }

    const regExp = "^[^@@\\s]+@@[^@@\\s]+\\.(com|net|org|gov|ru)$";

    if (!regExp.test(email)) {
        document.getElementById("emailError").innerText = "Email has incorrect input";

        valid = false;
        return valid;
    }

    if (password.length < 6) {
        document.getElementById("passwordError").innerText = "Password must be at least 6 characters";
        valid = false;
        return valid;
    }

    return valid; // Если valid = false, форма не отправится
}

function validateLoginForm() {
    let valid = true;

    // Очистка предыдущих сообщений
    document.querySelectorAll(".error-message").forEach(el => el.innerText = "");
    document.querySelectorAll(".error-message-from-server").forEach(el => el.innerText = "");

    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;

    let error = findEmptyValue({ "username": username, "password": password });

    if (error != null) {
        document.getElementById(error + "Error").innerText = error.replace(/^\w/, c => c.toUpperCase()) + " is required";
        valid = false;
        return valid;
    }

    if (username.length < 3) {
        document.getElementById("usernameError").innerText = "Username must be at least 3 characters long";
        valid = false;
        return valid;
    }

    if (password.length < 6) {
        document.getElementById("passwordError").innerText = "Password must be at least 6 characters";
        valid = false;
        return valid;
    }

    return valid; // Если valid = false, форма не отправится
}

function findEmptyValue(fields) {

    for (var fieldName in fields) {

        if (!fields.hasOwnProperty(fieldName)) continue;

        var fieldValue = fields[fieldName];

        if (fieldValue == "") {
            return fieldName;
        }
    }
}

function redirectToContacts() {
    const contactBookName = document.getElementById("contactBookName").innerHTML;

    window.location.href = "/ContactBook/Contacts?contact_book_name=" + contactBookName; 
}

async function delectContact(event) {
    event.preventDefault();

    const contactBookName = document.getElementById("contactBookName").innerHTML;
    const name = document.getElementById("name").value;
    const description = document.getElementById("description").value;
    const id = document.getElementById("id-contact").innerHTML;

    const formData = new FormData();
    formData.append("Name", name);
    formData.append("Description", description);
    formData.append("contact_book_name", contactBookName);
    formData.append("Id", id);

    await fetch("https://localhost:7054/Contact/Delete?contact_book_name=" + contactBookName, {
        method: "DELETE",
        body: formData
    }).then(response => {
        if (response.ok) {
            window.location.href = "https://localhost:7054/ContactBook/Contacts?contact_book_name=" + contactBookName;
        }
    });
}

async function createContact(event) {
    event.preventDefault();

    const contactBookName = document.getElementById("contactBookName").innerHTML;
    const name = document.getElementById("name").value;
    const description = document.getElementById("description").value;

    const formData = new FormData();
    formData.append("Name", name);
    formData.append("Description", description);
    formData.append("contact_book_name", contactBookName);

    await fetch("https://localhost:7054/Contact/CreateContact?contact_book_name=" + contactBookName, {
        method: "POST",
        body: formData
    }).then(response => {
        if (response.ok) {
            window.location.href = "https://localhost:7054/ContactBook/Contacts?contact_book_name=" + contactBookName;
        }
        });
}

function showEditForm() {

    var a = document.getElementById("contactName").innerHTML;
    var b = document.getElementById("contactDescription").innerHTML;
    var c = document.getElementById("id-contact").innerHTML
    var d = document.getElementById("contactBookName").innerHTML;
    document.getElementById("name-edit").value = a;
    document.getElementById("description-edit").value = b;
    document.getElementById("id-hidden").value = c;
    document.getElementById("contact-book-name-hidden").value = d;

    let div = document.getElementById("edit-contact-con");
    div.removeAttribute("hidden");
}

async function deleteContactBook() {
    const name = document.getElementById("contactBookName").innerHTML;

    await fetch("https://localhost:7054/ContactBook/Delete?nameContactBook=" + encodeURIComponent(name), {
        method: "DELETE",
        headers: {
            'Accept': 'application/json'
        }
    })
        .then(response => {
            if (response.ok) {
                window.location.href = "https://localhost:7054/ContactBook/Contacts"; // например, перенаправляем на список контактов
            }
        });

}
