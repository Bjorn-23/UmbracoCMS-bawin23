document.addEventListener('DOMContentLoaded', function () {
    openMenu();
    setAbsoluteBackgroundParentContainerHeight()
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

//function setAbsoluteBackgroundParentContainerHeight() {
//    console.log("inside function")
//    let backgrounds = document.querySelectorAll(".background-image-absolute");

//    backgrounds.forEach(background => {
//        console.log("looping through backgrounds")
//        let imgHeight = window.getComputedStyle(background).height;
//        console.log(imgHeight + "working");
//        let section = background.closest('section');

//        section.style.height = imgHeight
//    })

//}