var currentUser = JSON.parse(sessionStorage.getItem("user"));


window.addEventListener('DOMContentLoaded', event => {
    //Nav bar - Permission
    if (currentUser[1] == 4) {    //Volunteer is logged in
        $(".ManagerNav").hide();
        $(".AdminNav").hide();
        $(".VolunteerNav").show();
    }
    else if (currentUser[1] == 1) { //Admin is logged in
        $(".AdminNav").show();
        $(".ManagerNav").hide();
        $(".VolunteerNav").hide();
    }
    else {                        //Manager is logged in
        $(".ManagerNav").show();
        $(".AdminNav").hide();
        $(".VolunteerNav").hide();
    }

    $(".u39").mouseenter(UserEnterSubManu);
    $(".u39").mouseleave(UserExitSubManu);
    $(".u40").mouseleave(UserExitSubManu);

    $(".logout").click(logout);


    //-------------------------------------------------------------------------------------------------
    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('.mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '.mainNav',
            offset: 74,
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

});

//NAVBAR USER

function UserEnterSubManu() {
    $(".u40").css("visibility", "inherit")
    $(".u40").show();
}
function UserExitSubManu() {
    $(".u40").css("visibility", "hidden")
    $(".u40").hide();
}

//logout function
function logout() {
    isLogIn = false;
    sessionStorage.clear();
    window.location.assign("Log-In.html");
}

//END - NAVBAR USER