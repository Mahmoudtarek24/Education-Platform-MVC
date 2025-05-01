// If using Bootstrap tabs, no additional JS is needed
// If using custom tabs, use this:
document.addEventListener('DOMContentLoaded', function () {
    const tabLinks = document.querySelectorAll('.tab-link');

    tabLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();

            const targetTab = this.getAttribute('data-tab');
            const targetContent = document.getElementById(targetTab);

            // Hide all tab contents
            document.querySelectorAll('.tab-content').forEach(content => {
                content.classList.remove('active');
            });

            // Deactivate all tab links
            document.querySelectorAll('.tab-link').forEach(tab => {
                tab.classList.remove('active');
            });

            // Show selected tab
            targetContent.classList.add('active');
            this.classList.add('active');
        });
    });
});