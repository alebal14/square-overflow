import React from 'react';
import styles from '../styles/servererror.module.css';

interface ServerErrorMessageProps {
  statusCode: number;
}

export function ServerErrorMessage({ statusCode }: ServerErrorMessageProps) {
  return (
    <div className={`${styles.serverErrorInfo}`}>
      <h3>Server Error ({statusCode})</h3>
      <p>The server is currently unavailable. This could be due to:</p>
      <ul>
        <li>Server maintenance</li>
        <li>High traffic</li>
        <li>The API service needs to be started</li>
      </ul>
      <p>Please check that your API is running at https://localhost:7194</p>
    </div>
  );
}