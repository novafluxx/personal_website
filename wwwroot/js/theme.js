window.setThemeOnHtml = (theme) => {
    // First, apply the transitioning class to all elements
    // This ensures they're all prepared for the transition
    document.body.classList.add('theme-transitioning');
    
    // Use requestAnimationFrame to ensure all DOM updates happen in the same paint cycle
    requestAnimationFrame(() => {
        // Apply theme to root level
        document.documentElement.setAttribute('data-bs-theme', theme);
        
        // Force a reflow to ensure styles are computed before the transition starts
        document.documentElement.offsetHeight;
        
        // Remove transitioning class after transition completes
        setTimeout(() => {
            document.body.classList.remove('theme-transitioning');
        }, 350); // Match this to the transition duration in CSS
    });
};
