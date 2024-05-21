export function serializer(obj) {
    return Object.keys(obj)
        .filter(key => obj[key] !== null && obj[key] !== undefined)
        .map(key => encodeURIComponent(key) + '=' + encodeURIComponent(obj[key]))
        .join('&');
}