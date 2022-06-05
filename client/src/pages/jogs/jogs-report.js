import React from 'react';
import { normalizeTime } from '../../utils/time';

const getUserInfo = (user, currentUser) => {
  const result = `${user.firstName} ${user.lastName}`;

  if (currentUser === null || currentUser.id !== user.id) {
    return result;
  }

  return `${result} (you)`;
};

const JogsReport = ({ report, currentUser, showInKm }) => {
  return (
    <div className="w-100">
      <GeneralReport report={report} currentUser={currentUser} showInKm={showInKm} />
      {report.user && <PersonalReport report={report} showInKm={showInKm} />}
    </div>
  );
};

const GeneralReport = ({ report, currentUser, showInKm }) => (
  <table className="jogs-report__table">
    <thead>
      <tr>
        <th colSpan={3}>General statistics on jogging</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <th>Largest total distance covered</th>
        <td>{showInKm ? `${report.maxTotalDistanceInKilometers} km` : `${report.maxTotalDistanceInMeters} m`}</td>
        <td>{getUserInfo(report.maxTotalDistanceUser, currentUser)}</td>
      </tr>
      <tr>
        <th>Longest total jogging time</th>
        <td>{normalizeTime(report.maxTotalElapsedTime)}</td>
        <td>{getUserInfo(report.maxTotalElapsedTimeUser, currentUser)}</td>
      </tr>
      <tr>
        <th>Largest total average speed</th>
        <td>
          {showInKm
            ? `${report.maxAverageSpeedInKilometersPerHour} km/h`
            : `${report.maxAverageSpeedInMetersPerSecond} m/s`}
        </td>
        <td>{getUserInfo(report.maxAverageSpeedUser, currentUser)}</td>
      </tr>
      <tr>
        <th>Total jogs completed</th>
        <td colSpan={2}>{report.totalJogs}</td>
      </tr>
    </tbody>
  </table>
);

const PersonalReport = ({ report, showInKm }) => (
  <table className="jogs-report__table">
    <thead>
      <tr>
        <th colSpan={2}>Your personal statistics on jogging</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <th>Total distance covered</th>
        <td>{showInKm ? `${report.ownTotalDistanceInKilometers} km` : `${report.ownTotalDistanceInMeters} m`}</td>
      </tr>
      <tr>
        <th>Total jogging time</th>
        <td>{normalizeTime(report.ownTotalElapsedTime)}</td>
      </tr>
      <tr>
        <th>Total average speed</th>
        <td>
          {showInKm
            ? `${report.ownAverageSpeedInKilometersPerHour} km/h`
            : `${report.ownAverageSpeedInMetersPerSecond} m/s`}
        </td>
      </tr>
      <tr>
        <th>Total jogs completed</th>
        <td>{report.totalOwnJogs}</td>
      </tr>
    </tbody>
  </table>
);

export default JogsReport;
