// Order success alert
function showOrderSuccessAlert(message) {
    Swal.fire({
        title: 'Thành công!',
        text: message,
        icon: 'success',
        confirmButtonText: 'OK'
    });
}

// Check if there's a saved message in sessionStorage
$(document).ready(function() {
    const orderSuccessMessage = sessionStorage.getItem('orderSuccessMessage');
    if (orderSuccessMessage) {
        showOrderSuccessAlert(orderSuccessMessage);
        // Clear the message after showing it
        sessionStorage.removeItem('orderSuccessMessage');
    }
}); 