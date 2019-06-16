function displayCluster(dots) {
	for (const dot of dots) {
		putDot(dot.x, dot.y);
	}
}

function displayClusters(clusters) {
	drawingCtx.clearRect(0, 0, container.width, container.height);
	for (const cluster of clusters) {
		setCurrentColor(cluster.hue);
		displayCluster(cluster.Dots);
		putBigDot(cluster.center);
	}
}
