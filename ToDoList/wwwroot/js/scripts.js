$("#createCategoryForm").on("submit", function (e) {
	e.preventDefault();

	const categoryName = $("#categoryName").val();

	$.ajax({
		url: "/api/categories",
		method: "POST",
		contentType: "application/json",
		data: JSON.stringify({
			"name": categoryName
		})
	}).done(function () {
		const link = $("<a></a>", { href: "#", class: "btn btn-outline-secondary btn-block" });

		link.append(categoryName);

		$("#categoriesList").append(link);
	}).fail(function () {
		alert("Something failed.");
	});
});

$(".js-delete-category").on("click", function (e) {
	e.preventDefault();

	const categoryId = $(e.target).data("category-id");

	$.ajax({
		url: `/api/categories/${categoryId}`,
		method: "DELETE",
		contentType: "application/json",
	}).done(function () {
		$("#manageCategories").remove();
	}).fail(function () {
		alert("Something failed.");
	});
});
