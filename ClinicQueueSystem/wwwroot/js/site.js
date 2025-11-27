window.appStorage = {
  get: function (key) {
    try {
      return window.localStorage.getItem(key);
    } catch (e) {
      return null;
    }
  },
  set: function (key, value) {
    try {
      window.localStorage.setItem(key, value);
    } catch (e) {
      // ignore
    }
  }
};


