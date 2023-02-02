/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */

let eventGuid = 0;

export function createEventId() {
    return String(eventGuid++);
}

export const getTypeById = (type) => {
    switch (type) {
        case 1:
            return "Walk";
        case 2:
            return "Vet";
        case 3:
            return "Grooming";
        default:
            return "Other";
    }
};

export const getColorByTypeId = (type) => {
    switch (type) {
        case 1:
            return "green";
        case 2:
            return "red";
        case 3:
            return "gray";
        default:
            return "blue";
    }
}