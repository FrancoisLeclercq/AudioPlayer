// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Dark mode.
$("#darkTrigger").click(function () {
    if ($("body").hasClass("dark")) {
        $("body").removeClass("dark");
        localStorage.setItem('mode', 'light');
    }
    else {
        $("body").addClass("dark");
        localStorage.setItem('mode', 'dark');
    }
});

window.onload = function () {
    if (localStorage.getItem('mode') == 'dark') {
        $("body").addClass("dark");
    }
}