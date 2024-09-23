document.addEventListener('DOMContentLoaded', function () {
    openMenu();
})


function openMenu() {
    const mobileBtn = document.querySelector('#menuBars');

    mobileBtn.addEventListener('click', () => {
        const mobileMenu = document.querySelector('#mobileMenu');

        console.log("click")
        const isOpen = mobileMenu.getAttribute('aria-expanded') === 'true';

        mobileMenu.setAttribute('aria-expanded', !isOpen)
        mobileBtn.querySelector('i').classList.toggle('fa-bars', isOpen);
        mobileBtn.querySelector('i').classList.toggle('fa-plus', !isOpen);
        if (mobileBtn.querySelector('i').classList.contains('fa-plus')) {
            mobileBtn.style.transform = 'rotate(45deg)';
        } else {
            mobileBtn.style.transform = 'rotate(0deg)';

        }
    })
}