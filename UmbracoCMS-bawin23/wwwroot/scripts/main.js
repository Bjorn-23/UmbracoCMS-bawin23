document.addEventListener('DOMContentLoaded', function () {
    openMenu();
})

window.onload = function () {
    setParentHeightFromFormHeight()
    createdProjectCarousel()
}

window.addEventListener("resize", function () {
    setParentHeightFromFormHeight()
    createdProjectCarousel()
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
        //console.log("if")
        let formHeight = parseFloat(window.getComputedStyle(form).height);
        //This adds a fractional margin to the space between the section end and bottom of the form.
        //let calulatedSectionHeight = Math.ceil(formHeight + (formHeight / 30))
        section.style.height = formHeight + "px";
    }
    else {
        //console.log("else")
        section.style.height = "inherit";
    }
}

function createdProjectCarousel() {
    let projectContainer = document.querySelector(".element-project-grid-container");
    let project = document.querySelector(".grid-box-wrapper");
    let slides = document.querySelectorAll(".grid-box-wrapper");
    let nextSlide = document.querySelector(".next-slide");
    let prevSlide = document.querySelector(".prev-slide");
    let windowWidth = window.innerWidth

    if (windowWidth < 768 && slides.length > 0) {
        console.log("if")
        let projectHeight = parseFloat(window.getComputedStyle(project).height);
        projectContainer.style.height = projectHeight + "px";
        let i = 0;
        let currentSlide = slides[i];

        HideSlides(slides, currentSlide);

        nextSlide.addEventListener("click", function (e) {
            e.preventDefault();
            //console.log("next")
            //console.log(i)
            if (i >= Math.ceil(slides.length -1)) {
                i = 0
                currentSlide = slides[i]
            }
            else {
                i = i + 1
                currentSlide = slides[i];
            }
            //console.log(slides)
            HideSlides(slides, currentSlide);
        })

        prevSlide.addEventListener("click", function (e) {
            e.preventDefault();
            //console.log("prev")
            //console.log(i)
            if (i <= 0) {
                i = slides.length -1
                currentSlide = slides[i]
            }
            else {
                i = i - 1;
                currentSlide = slides[i];
            }
            //console.log(slides)
            HideSlides(slides, currentSlide);
        })        
    }
    else {
        //console.log("else")
        projectContainer.style.height = "inherit";
        slides.forEach(slide => {
            if (slide.classList.contains("hide-project")) {
                slide.classList.remove("hide-project")
            }            
        })
    }
}

function HideSlides(slides, currentSlide) {
    slides.forEach(slide => {
        if (slide === currentSlide) {
            if (slide.classList.contains("hide-project")) {
                slide.classList.remove("hide-project")
                slide.classList.add("active-project")                
            }                       
        }
        else {
            if (slide.classList.contains("active-project")) {
                slide.classList.remove("active-project")
            }
            slide.classList.add("hide-project")
        }
        //console.log(slide)
    })
}



