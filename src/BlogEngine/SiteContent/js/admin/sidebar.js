jQuery(function ($) {
    $(".sidebar-dropdown > a").click(function () {
        var isActive = $(this).parent().hasClass("active");

        $(".sidebar-dropdown").removeClass("active");
        $(".sidebar-submenu").slideUp(200);

        if (!isActive) {
            $(this)
                .next(".sidebar-submenu")
                .slideDown(200);
            $(this)
                .parent()
                .addClass("active");
        }
    });
    $("#close-sidebar").click(function () {
        $(".page-wrapper").removeClass("toggled");
    });
    $("#show-sidebar").click(function () {
        $(".page-wrapper").addClass("toggled");
    });
});

jQuery(document).ready(function ($) {
    var submenu = $("a.active").first().closest(".sidebar-submenu");
    submenu.slideDown(0);
    submenu.parent().addClass("active");
})