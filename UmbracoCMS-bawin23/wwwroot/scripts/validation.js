document.addEventListener('DOMContentLoaded', function () {
    formValidation();
})

function formValidation() {
    let form = document.querySelectorAll("form");

    form.forEach(form => {
        let inputs = form.querySelectorAll("input")

        inputs.forEach(input => {
            if (input.type === "email") {
                console.log("email")
                //logic here for validating input.
            }
            if (input.type === "text") {
                console.log("text")
                ///logic here for validating input.
            }            
        })
    })
}