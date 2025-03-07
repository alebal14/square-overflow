import React from 'react';
import styles from '../styles/errorbanner.module.css';
import { ErrorState } from '../interfaces';

interface ErrorBannerProps {
  error: ErrorState;
  onClose: () => void;
}

export function ErrorBanner({ error, onClose }: ErrorBannerProps) {
  const getErrorMessage = () => {
    switch (error.statusCode) {
      case 400:
        return 'Bad request. Please check your input.';
      case 401:
        return 'Unauthorized. Please log in again.';
      case 403:
        return 'Forbidden. You don\'t have permission to access this resource.';
      case 404:
        return 'Resource not found. The API endpoint might have changed.';
      case 500:
        return 'Internal server error. Please try again later.';
      case 501:
        return 'Not implemented. This feature is not available yet.';
      case 503:
        return 'Service unavailable. The server may be down for maintenance.';
      default:
        return error.message || 'An unexpected error occurred.';
    }
  };

  return (
    <div className={`${styles.errorBanner}`}>
      <p>{getErrorMessage()}</p>
      <button 
        onClick={onClose}
        className={`${styles.btnClose}`}
      >
        ✕
      </button>
    </div>
  );
}