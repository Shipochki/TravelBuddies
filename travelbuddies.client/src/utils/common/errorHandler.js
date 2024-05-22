export const errorHandler = (statusCode) => {
    if(statusCode == 404){
        window.location.assign('/notFound');
    }else if(statusCode == 403){
        window.location.assign('/forbidden');
    }else if(statusCode == 400){
        window.location.assign('/badRequest');
    }
}