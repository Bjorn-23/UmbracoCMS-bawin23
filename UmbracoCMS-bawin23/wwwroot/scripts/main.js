document.addEventListener('DOMContentLoaded', function () {
    openMenu();
    setParentHeightFromFormHeight()
})

window.addEventListener("resize", function () {
    setParentHeightFromFormHeight()
})


function openMenu() {
    const mobileBtn = document.querySelector('#menuBars');

    mobileBtn.addEventListener('click', () => {
        const mobileMenu = document.querySelector('#mobileMenu');

        //These 2 lines are responsible for opening the menu
        const isOpen = mobileMenu.getAttribute('aria-expanded') === 'true';
        mobileMenu.setAttribute('aria-expanded', !isOpen)

        //These 2 lines toggle whether the icon is fa-bars or fa-plus dependent on isOpen.
        mobileBtn.querySelector('i').classList.toggle('fa-bars', isOpen);
        mobileBtn.querySelector('i').classList.toggle('fa-plus', !isOpen);

        //These 2 lines rotate the plus sign 45Deg to turn it into an X, and back to 0 when the bars are displayed.
        if (mobileBtn.querySelector('i').classList.contains('fa-plus')) {
            mobileBtn.style.transform = 'rotate(45deg)';
        } else {
            mobileBtn.style.transform = 'rotate(0deg)';

        }
    })
}

function setParentHeightFromFormHeight() {
    let form = document.querySelector(".form-wrapper");
    let section = form.closest('section');
    let windowWidth = window.innerWidth

    if (section.id == "about-us" && windowWidth > 1399) {
        console.log("if")
        let formHeight = parseFloat(window.getComputedStyle(form).height);
        //This adds a fractional margin to the space between the section end and bottom of the form.
        //let calulatedSectionHeight = Math.ceil(formHeight + (formHeight / 30))
        section.style.height = formHeight + "px";
    }
    else {
        console.log("else")
        section.style.height = "inherit";
    }
}

