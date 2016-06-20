
/**
 * @constructor
 */
function TwoinonePosture(tioExtension) {

    this._tioExtension = tioExtension;
    this._tioExtension.monitorTablet(function (isTablet) {
        if (isTablet && !this._updateCalled) {
            // Emit tablet posture only if update() has never
            // been called with coordinates.
            this._emit(TwoinonePosture.TABLET);
        } else {
            this._emit(TwoinonePosture.LAPTOP);
        }
    }.bind(this));

    if (tioExtension.isTablet()) {
        this._orientation = TwoinonePosture.TABLET;
    } else {
        this._orientation = TwoinonePosture.LAPTOP;
    }

    this._listeners = [];
    this._updateCalled = false;
}

Object.defineProperty(TwoinonePosture, "LAPTOP", {
    get: function() {
        return 0;
    }
});

Object.defineProperty(TwoinonePosture, "TABLET", {
    get: function() {
        return 1;
    }
});

Object.defineProperty(TwoinonePosture, "TENT", {
    get: function() {
        return 2;
    }
});

Object.defineProperty(TwoinonePosture, "CURTAIN", {
    get: function() {
        return 3;
    }
});

Object.defineProperty(TwoinonePosture.prototype, "orientation", {
    get: function() {
        return this._orientation;
    }
});

/**
 * Process current orientation.
 */
TwoinonePosture.prototype.update =
function(alpha, beta, gamma) {

    this._updateCalled = true;

    if (this._tioExtension &&
        !this._tioExtension.isTablet()) {
        this._emit(TwoinonePosture.LAPTOP);
    }

    // Tent
    // alpha -85 .. -20
    // beta -45 .. 45
    if (alpha >= -85 && alpha <= -20 &&
        beta  >= -45 && beta  <=  45) {

        this._emit(TwoinonePosture.TENT);
        return;
    }

    // Curtain
    // beta 80 .. 90, -89 .. -80
    if (beta >=  80 && beta <=  90 ||
        beta >= -89 && beta <= -80 ) {

        this._emit(TwoinonePosture.CURTAIN);
        return;
    }

    this._emit(TwoinonePosture.TABLET);
};

/**
 * @private
 */
TwoinonePosture.prototype._emit =
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
TwoinonePosture.prototype.onchanged =
function(callback) {

    this._listeners.push(callback);
};

/**
 * @constructor
 */
function TwoinoneExtensionEmulation(isTablet) {

    this._isTablet = isTablet ? true : false;
    this._listeners = [];
}

/**
 * Implementation of the corresponing extension function.
 */
TwoinoneExtensionEmulation.prototype.isTablet =
function() {
    return this._isTablet;
};

/**
 * Implementation of the corresponing extension function
 * which emulates switches through the extension.
 */
TwoinoneExtensionEmulation.prototype.setIsTablet =
function(value) {

    var boolValue = value ? true : false;
    if (boolValue != this._isTablet) {
        this._isTablet = boolValue;
        this._emit();
    }
};

/**
 * Implementation of the corresponing extension function.
 */
TwoinoneExtensionEmulation.prototype.monitorTablet =
function(callback) {
    this._listeners.push(callback);
};

/**
 * @private
 */
TwoinoneExtensionEmulation.prototype._emit =
function() {

    this._listeners.forEach(function (callback) {
        callback(this._isTablet);
    }.bind(this))
};
