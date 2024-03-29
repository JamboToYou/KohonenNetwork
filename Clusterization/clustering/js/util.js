function getRandomInt(min, max) {
	return Math.floor(Math.random() * (max - min)) + min;
}

function polymorph() {
	var len2func = [];
	for (var i = 0; i < arguments.length; i++)
		if (typeof (arguments[i]) == "function")
			len2func[arguments[i].length] = arguments[i];
	return function () {
		return len2func[arguments.length].apply(this, arguments);
	}
}