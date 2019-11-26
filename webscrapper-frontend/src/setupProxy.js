const proxy = require("http-proxy-middleware");

module.exports = function(app) {
    app.use(
        proxy("/api/etl/transform", {
            target: "http://localhost:54985",
            secure: false,
            changeOrigin: true
        })
    )
}