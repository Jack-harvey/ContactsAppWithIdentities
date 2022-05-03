//const bootstrap = require("../lib/bootstrap/dist/js/bootstrap");

const themeSwitch = document.getElementById('theme-toggle');
themeSwitch.addEventListener('click', () => {
    const newTheme = document.body.classList.contains('dracula-theme') ? 'atom-one-light-theme' : 'dracula-theme';

    const toastclasses = ['bg-primary', 'bg-success', 'bg-danger'];
    var documentToastId = document.getElementById('theme-toast');
    var themeIconId = document.getElementById('theme-icon')
    /*var baseUrl = "https://localhost:7063";*/

    toastclasses.forEach((c) => documentToastId.classList.remove(c));
    /*$.post((baseUrl.concat("/Home/SaveNewThemePreference")), { themePreference: newTheme },*/
    $.post ("/Home/SaveNewThemePreference", { themePreference: newTheme },
        function (data, status, jqxhr) {
            if (data.result == 1) {
                removeBodyThemes();
                removeThemeIcons();
                document.body.classList.add(data.theme);
                documentToastId.classList.add('bg-success');
                themeIconId.classList.add(data.icon);

            } else {
                documentToastId.classList.add('bg-danger');
            }
            documentToastId.querySelector('.toast-body').innerHTML = data.message;
            var toastPopsUp = new bootstrap.Toast(documentToastId);
            toastPopsUp.show();
        }
    );
});

function removeBodyThemes() {
    document.body.classList.forEach((c) => {
        if (c.endsWith('theme'))
            document.body.classList.remove(c);
    });
}

function removeThemeIcons() {
    document.getElementById('theme-icon').classList.forEach((c) => {
        if (c.startsWith('fa-sun' || 'fa-moon'))
            document.getElementById('theme-icon').classList.remove(c);
    })
}