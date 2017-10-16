// Currency Selection Objects
class $cy_item {
	constructor(args) {
		this.ticker = args[0];
		this.name = args[1];
		this.sprite = args[2];
		this.pairings = [];
	}
	buildPairs() {
		for (let pair of currency.pairs) {
			this._buildPair(pair, 0, 1, false);
			this._buildPair(pair, 1, 0, true);
		}
	}
	_buildPair(pair, index1, index2, reversed) {
		if (this.equals(pair[index1])) {
			this.pairings.push(pair[index2]);
			this[pair[index2].ticker] = reversed;
		}
	}
	equals(other) {
		return this.ticker == other.ticker;
	}
	pairs(other) {
		return !(typeof this[other.ticker] === "undefined");
	}
}
class $cy_list {
	constructor() {
		this.list = [];
		this.pairs = [];
	}
	add() {
		for (let arg = 0; arg < arguments.length; arg++) {
			this[arguments[arg][0]] = new $cy_item(arguments[arg]);
			this.list.push(this[arguments[arg][0]]);
		}
	}
	addPairs() {
		for (let arg = 0; arg < arguments.length; arg++) {
			let pair = arguments[arg].split('|');
			this.pairs.push([this[pair[0]], this[pair[1]]]);
		}
		for (let item of this.list) item.buildPairs();
	}
}
class currencyPair {
	constructor() {
		this.first = null;
		this.firstChoices = currency.list;
		this.second = null;
		this.secondChoices = currency.list;
	}
	isValid() {
		return this.first !== null && this.second !== null && this.first.pairs(this.second);
	}
	change(isFirst) {
		if (isFirst) this.secondChoices = this.first == null ? currency.list : this.first.pairings;
		if (!this.isValid()) return;
		this.reversed = this.first[this.second.ticker]
		this.ticker = this.reversed ? this.second.ticker + '-' + this.first.ticker : this.first.ticker + '-' + this.second.ticker;
		console.log(this.ticker + ':' + (this.reversed ? 'short' : 'long'));
	}
}
var currency = new $cy_list();