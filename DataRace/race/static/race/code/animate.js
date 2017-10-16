class sprite {
	constructor(canvasID, path, nFrames, wFrames, spriteWidth, spriteHeight, scale, nTicks, y) {
		this.spritesheet = new Image();
		this.spritesheet.name = path;
		this.spritesheet.src = path;
		this.nFrames = nFrames;
		this.wFrames = wFrames;
		this.width = spriteWidth;
		this.height = spriteHeight;
		this.canvas = document.getElementById(canvasID);
		this.currFrame = 0;
		this.tick = 0;
		this.nTicks = nTicks;
		this.draw(0);
		this.y = y;
		this.scale= scale;
	}
	draw(currFrame) {
		this.currFrame = currFrame % this.nFrames;
		let sourcex = (this.currFrame % this.wFrames) * this.width;
		let sourcey = Math.floor(this.currFrame / this.wFrames) * this.height;
		//this.canvas.getContext("2d").clearRect(0, 0, this.canvas.width, this.canvas.height);
		this.canvas.getContext("2d").drawImage(this.spritesheet, sourcex, sourcey, this.width, this.height, 0, this.y, this.width * this.scale, this.height * this.scale);
	}
	drawNext() {
		this.draw(this.currFrame + 1);
	}
	render() {
		this.tick = (this.tick + 1) % this.nTicks;
		if (this.tick == 0) this.drawNext();
	}
}