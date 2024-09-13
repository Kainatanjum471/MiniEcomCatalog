import { apiConfig } from './Common.js';
// Get All Products
function getAllProducts() {
    $.ajax({
        url: "/api/product",
        method: "GET",
        success: function (data) {
            console.log(data);
            // Render data to the view
        },
        error: function (err) {
            console.error(err);
        }
    });
}

// Get Product By ID
function getProductById(id) {
    $.ajax({
        url: "/api/product/" + id,
        method: "GET",
        success: function (data) {
            console.log(data);
            // Render product details to the view
        },
        error: function (err) {
            console.error(err);
        }
    });
}

// Add Product
function addProduct(product) {
    $.ajax({
        url: "/api/product",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(product),
        success: function (data) {
            console.log("Product added successfully");
            // Redirect or update the view
        },
        error: function (err) {
            console.error(err);
        }
    });
}

// Update Product
function updateProduct(id, product) {
    $.ajax({
        url: "/api/product/" + id,
        method: "PUT",
        contentType: "application/json",
        data: JSON.stringify(product),
        success: function () {
            console.log("Product updated successfully");
            // Redirect or update the view
        },
        error: function (err) {
            console.error(err);
        }
    });
}

// Delete Product
function deleteProduct(id) {
    $.ajax({
        url: "/api/product/" + id,
        method: "DELETE",
        success: function () {
            console.log("Product deleted successfully");
            // Update the view
        },
        error: function (err) {
            console.error(err);
        }
    });
}

// Search Products
function searchProducts(name, category) {
    $.ajax({
        url: "/api/product/search",
        method: "GET",
        data: { name: name, category: category },
        success: function (data) {
            console.log(data);
            // Render search results to the view
        },
        error: function (err) {
            console.error(err);
        }
    });
}

