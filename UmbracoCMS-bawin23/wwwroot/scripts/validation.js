document.addEventListener('DOMContentLoaded', function () {
    formValidation();
})

function formValidation() {
    let form = document.querySelectorAll("form");

    form.forEach(form => {
        let inputs = form.querySelectorAll("input")

        inputs.forEach(input => {
            input.addEventListener('keyup', (e) => {
                //console.log(e)

                switch (e.target.type) {
                    case 'text':
                        if (e.target.id === "formPhone") {
                            validatePhone(e);
                        } else {
                            validateText(e);
                        }
                        break;

                    case 'email':
                        emailValidation(e);
                        break;
                }
            });          
        })
    })
}

function lengthControl(event, minLength) {
    if (event.target.value.length < minLength) {
        return false
    }
    if (event.target.id === "formPhone") {
        const phoneRegEx = /^\+?\d[\d\s]{7,20}$/;
        return phoneRegEx.test(event.target.value);
    }
    else {
        return true;
    }
}

function validateText(event) {
    formErrorHandler(event, lengthControl(event, 2));
}

function validatePhone(event) {
    formErrorHandler(event, lengthControl(event, 8));
}



function emailValidation(event) {
    const regEx = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,63}$/;
    formErrorHandler(event, regEx.test(event.target.value));
}

function formErrorHandler(event, result) {
    console.log(result)
    let spanElement = document.querySelector(`span[for="${event.target.id}"]`);
    if (result) {
        spanElement.classList.remove("form-invalid-input");
    }
    else {
        spanElement.classList.add("form-invalid-input");
    }
}
