import React from 'react';
import styles from '../styles/loadingspinner.module.css';


export function LoadingSpinner() {
  return (
    <div className={`${styles.loaderOverlay}`}>
      <div className={`${styles.loader}`}>
        <p className={`${styles.loaderText}`}>
          Loading the Squareuniverse!
        </p>
        <div className={`${styles.loaderSpinner}`}></div>     
      </div>      
    </div>
  );
}