document.addEventListener("DOMContentLoaded", function () {
    serviceCardSlider();
})

window.onload = () => {
    serviceCardSlider();
}

window.addEventListener("resize", () => {
    serviceCardSlider();
})

function serviceCardSlider() {
    const serviceCardContainer = document.querySelector(".service-grid-container");
    const serviceCard = document.querySelector(".service-card");
    let serviceCardHeight = parseFloat(window.getComputedStyle(serviceCard).height);
    let closestSection = serviceCard.closest("section");

    if (window.innerWidth > 768) {
        return
    }
    if (closestSection === "our-services-index") {
        console.log(closestSection)
        serviceCardContainer.style.height = Math.ceil(serviceCardHeight) + "px";
    }
}