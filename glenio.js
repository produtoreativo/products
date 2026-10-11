const { utcToZonedTime, getTimezoneOffset } = require('date-fns-tz');
const { format } = require('date-fns');

const createDateInTimezone = (timezone) => {
  const date = new Date();
  console.log(`Current UTC time: ${date.toISOString()}`);
  
  const zonedDate = utcToZonedTime(date, timezone);
  const offset = getTimezoneOffset(timezone, date);
  const adjustedDate = new Date(zonedDate.getTime() - offset);
  
  const formattedDate = format(adjustedDate, "yyyy-MM-dd'T'HH:mm:ss.SSSX");
  return formattedDate;
};

const timezone = 'America/Mexico_City';
const dateInTimezone = createDateInTimezone(timezone);
console.log(`Date in ${timezone}: ${dateInTimezone}`);
