#!/usr/bin/env node

/* eslint no-extend-native : 0 */


// Bad juju, I know
Number.prototype.format = function () {
	return Math.round(this).toString().padStart(4, ' ');
};


function calcRGB(min, max, value) {
	if (value < min) value = min;
	if (value > max) value = max;

	// RGB multiplier calculation
	const rgb_pct = 100 * ((value - min) / (max - min));

	const value1 = (510 * ((value - min) / ((max - min)))) - 255;
	const value2 = Math.abs(value1);
	const value3 = 255 - value2;

	const data = { rgb_pct, value1, value2, value3 };

	// More red, less blue
	if (rgb_pct > 49.9) {
		return formatRGB(value1, value3, 0, data);
	}

	// More blue, less red
	return formatRGB(0, value3, value2, data);
}

function displayRGB(min, max, value) {
	const output_v2 = calcRGB(min, max, value);

	const c = {
		in : value.toString().padStart(3, ' '),
		...output_v2,
	};

	const color = {
		r : parseInt(c.r),
		g : parseInt(c.g),
		b : parseInt(c.b),
	};

	const ex = '\x1b[38;2;' + color.r + ';' + color.g + ';' + color.b + 'm';
	const rs = '\x1b[0m';


	console.log('%s || [' + ex + ' %s%% ' + rs + '] [ %s %s %s ] [' + ex + ' %s %s %s ' + rs + '] || %s', c.in, c.pct, c.v1, c.v2, c.v3, c.r, c.g, c.b, c.in);
}

function formatRGB(r, g, b, data) {
	return {
		pct : data.rgb_pct.toFixed(1).padStart(5, ' '),

		v1 : data.value1.format(),
		v2 : data.value2.format(),
		v3 : data.value3.format(),

		r : r.format(),
		g : g.format(),
		b : b.format(),
	};
}

function runRGB() {
	const min = Number(process.argv[2]);
	const max = Number(process.argv[3]);

	for (let value = min; value <= max; value++) {
		if (value % 2 !== 0) continue;
		displayRGB(min, max, value);
	}

	console.log();

	for (let value = max; value >= min; value--) {
		if (value % 2 !== 0) continue;
		displayRGB(min, max, value);
	}
}


runRGB();
