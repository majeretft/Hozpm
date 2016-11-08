function callZoom() {
	$('img[data-zoom-image]').elevateZoom({
		responsive: true,
		zoomType: 'inner',
		cursor: 'pointer'
	});
}

$(function () {
	callZoom();

	$(window).resize(function () {
		$('.zoomContainer').remove();
		callZoom();
	});
});