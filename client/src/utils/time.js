export const normalizeTime = (time) =>
  `${normalizePart(time.hours)}:${normalizePart(time.minutes)}:${normalizePart(time.seconds)}`;

const normalizePart = (part) => (part.toString().length < 2 ? `0${part}` : part);