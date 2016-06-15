
/**
 * @constructor
 */
function TwoinoneOrientation() {

    this._orientation = TwoinoneOrientation.UNINITIALIZED;

    this._listeners = [];
}

/**
 * @constant
 */
TwoinoneOrientation.UNINITIALIZED = -1;

/**
 * @constant
 */
TwoinoneOrientation.UNDEFINED = 0;

/**
 * @constant
 */
TwoinoneOrientation.TENT = 1;

/**
 * @constant
 */
TwoinoneOrientation.CURTAIN = 2;

/**
 * Process current orientation.
 */
TwoinoneOrientation.prototype.update =
function(alpha, beta, gamma) {

    // Tent
    // alpha -85 .. -20
    // beta -45 .. 45
    if (alpha >= -85 && alpha <= -20 &&
        beta  >= -45 && beta  <=  45) {

        this._emit(TwoinoneOrientation.TENT);
        return;
    }

    // Curtain
    // beta 80 .. 90, -89 .. -80
    if (beta >=  80 && beta <=  90 ||
        beta >= -89 && beta <= -80 ) {

        this._emit(TwoinoneOrientation.CURTAIN);
        return;
    }

    this._emit(TwoinoneOrientation.UNDEFINED);
};

/**
 * @private
 */
TwoinoneOrientation.prototype._emit =
function(orientation) {

    if (orientation != this._orientation) {
        this._orientation = orientation;
        this._listeners.forEach(function (callback) {
            callback(orientation);
        });
    }
};

/**
 * Connect event handler.
 */
TwoinoneOrientation.prototype.onchanged =
function(callback) {

    this._listeners.push(callback);
};
