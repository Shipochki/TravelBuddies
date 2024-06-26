export function parseJwt(token) {
    const [, payloadBase64] = token.split('.');
    const payload = JSON.parse(atob(payloadBase64));

    return { 
        nameId: payload.nameid, 
        sub: payload.sub, 
        role: payload.role,
        fullname: payload.fullname,
        exp: payload.exp,
        profilePictureLink: payload.profilePictureLink,
     };
}