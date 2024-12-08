// Global variable to cache the chart instance
window.chartCache;

// Function to set up a chart with the given configuration
window.setupChart = (id, config) => {
    // Destroy any existing chart instance to avoid duplicates
    destroyChart();

    // Get the 2D rendering context of the canvas element with the given ID
    let ctx = document.getElementById(id).getContext('2d');
    
    // Add configuration options for the chart
    config.options = {
        plugins: {
            title: {
                display: true, // Display the chart title
                text: 'Unemployment Data in the US by State or City', // Chart title text
                font: {
                    size: 16 // Font size for the title
                }
            },
            legend: {
                display: true // Display the legend for the datasets
            },
            zoom: {
                pan: {
                    enabled: true, // Enable panning
                    mode: 'xy', // Allow panning in both X and Y directions
                    scaleMode: 'xy' // Match the scaling mode
                },
                zoom: {
                    pinch: {
                        enabled: true // Enable zooming via touch gestures
                    },
                    wheel: {
                        enabled: true // Enable zooming via mouse wheel
                    },
                    mode: 'xy' // Allow zooming in both X and Y directions
                }
            }
        },
        mantainAspectRatio: false, // Disable maintaining aspect ratio
        responsive: true // Enable responsiveness for resizing
    };

    // Create a new Chart instance and cache it in the global variable
    window.chartCache = new Chart(ctx, config);
};

// Function to destroy the current chart instance if it exists
window.destroyChart = () => {
    if (window.chartCache != undefined) {
        window.chartCache.destroy(); // Destroy the chart
    }
};

// Function to save the chart as an image file
window.saveChartAsImage = (id) => {
    const canvas = document.getElementById(id); // Get the canvas element
    const image = removeTransparency(canvas).replace("image/png", "image/octet-stream"); // Remove transparency and prepare for download
    const link = document.createElement("a"); // Create a link element
    link.href = image; // Set the image as the link's href
    link.download = "chart.png"; // Set the default filename
    link.click(); // Trigger a download
};

// Function to save the current chart's zoom and viewport settings
window.saveChartBookmark = () => {
    if (window.chartCache == undefined) {
        return {}; // Return an empty object if no chart exists
    }

    // Get the current zoomed scale bounds from the chart
    let bounds = chartCache.getZoomedScaleBounds();
    let xmin, xmax, ymin, ymax;

    // Handle cases where X-axis bounds are undefined
    if (bounds.x == undefined) {
        let baseX = chartCache.getInitialScaleBounds().x; // Get the initial X-axis bounds
        xmin = baseX.min;
        xmax = baseX.max;
    } else {
        xmin = bounds.x.min;
        xmax = bounds.x.max;
    }

    // Handle cases where Y-axis bounds are undefined
    if (bounds.y == undefined) {
        let baseY = chartCache.getInitialScaleBounds().y; // Get the initial Y-axis bounds
        ymin = baseY.min;
        ymax = baseY.max;
    } else {
        ymin = bounds.y.min;
        ymax = bounds.y.max;
    }

    // Create a chart state object with the current bounds
    var chartState = {
        xmin: xmin, xmax: xmax,
        ymin: ymin, ymax: ymax
    };

    return chartState; // Return the chart state
};

// Function to restore the chart's viewport from a saved state
window.restoreChartViewport = (chartState) => {
    if (window.chartCache == undefined) {
        return; // Exit if no chart exists
    }

    // Reset the zoom if the chart is already zoomed
    let bounds = chartCache.getZoomedScaleBounds();
    if (bounds.x != undefined || bounds.y != undefined) resetZoom();

    // Extract the saved state bounds
    const { xmin, xmax, ymin, ymax } = chartState;

    // Restore the zoomed scale for X and Y axes
    chartCache.zoomScale('x', { min: xmin, max: xmax }, 'resize');
    chartCache.zoomScale('y', { min: ymin, max: ymax }, 'resize');
};

// Function to set the chart's zoom access
window.setZoomMode = (access) => {
    chartCache.config.options.plugins.zoom.zoom.mode = access;
};

// Helper function to remove transparency from the chart canvas
function removeTransparency(canvas) {
    const context = canvas.getContext('2d');
    const w = canvas.width;
    const h = canvas.height;

    context.save(); // Save the current canvas state

    context.globalCompositeOperation = "destination-over"; // Draw beneath existing content
    context.fillStyle = "#FFFFFF"; // Use a white background
    context.fillRect(0, 0, w, h); // Fill the entire canvas with white

    const imageData = canvas.toDataURL("image/png"); // Export the canvas to a PNG image

    context.restore(); // Restore the canvas state

    return imageData; // Return the image data
}

// Function to reset the chart's zoom level
window.resetZoom = (id) => {
    if (window.chartCache != undefined) {
        window.chartCache.resetZoom(); // Reset the zoom on the chart
    }
};

// Event listener to handle window resizing and resize the chart accordingly
window.addEventListener('resize', function () {
    if (window.chartCache != undefined) {
        window.chartCache.resize(); // Resize the chart to fit the new dimensions
    }
});