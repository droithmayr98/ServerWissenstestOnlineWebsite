/// <binding BeforeBuild='sass, min' />
var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    htmlmin = require("gulp-htmlmin"),
    uglify = require("gulp-uglify"),
    merge = require("merge-stream"),
    del = require("del"),
    sass = require('gulp-sass'),
    bundleconfig = require('./bundleconfig.json'),
    babel = require('gulp-babel');

gulp.task("min", ["min-site:js", "min:css", "min-admin:js", 'min-date:js', 'min-otherFunctions:js']);

gulp.task('sass', () => {
    var paths_sass = './wwwroot/css/**/*.scss';
    return gulp.src(paths_sass)
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('min-site:js', () => {
    return gulp.src('./wwwroot/js/site.js', { base: '.' })   
        .pipe(babel({'presets': ['es2015']}))
        .pipe(concat('./wwwroot/js/site.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('min-admin:js', () => {
    return gulp.src('./wwwroot/js/admin.js', { base: '.' })
        .pipe(babel({ 'presets': ['es2015'] }))
        .pipe(concat('./wwwroot/js/admin.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('min-date:js', () => {
    return gulp.src('./wwwroot/js/date.js', { base: '.' })
        .pipe(babel({ 'presets': ['es2015'] }))
        .pipe(concat('./wwwroot/js/date.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('min-otherFunctions:js', () => {
    return gulp.src('./wwwroot/js/otherFunctions.js', { base: '.' })
        .pipe(babel({ 'presets': ['es2015'] }))
        .pipe(concat('./wwwroot/js/otherFunctions.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task('min:css', ['sass'] , () => {
    const paths = './wwwroot/css/**/*.css';
    const pathMin = './wwwroot/css/site.min.css';
    return gulp.src(paths, { base: '.' })
        .pipe(concat(pathMin))
        .pipe(cssmin())
        .pipe(gulp.dest('.'));
});


gulp.task("clean", () => del(['./wwwroot/js/**/*.min.js',
    './wwwroot/css/**/*.min.css']));

gulp.task("watch", () => {
    gulp.watch('./wwwroot/js/site.js', ['min-site:js']);
    gulp.watch('./wwwroot/js/admin.js', ['min-admin:js']);
    gulp.watch('./wwwroot/js/date.js', ['min-date:js']);
    gulp.watch('./wwwroot/js/otherFunctions.js', ['min-otherFunctions:js']);
    gulp.watch('./wwwroot/css/**/*.css', ['min:css']);
});