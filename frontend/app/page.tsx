"use client";
import React from 'react';
import styles from './styles/squarepage.module.css';
import Image from "next/image";
import { LoadingSpinner } from './components/loadingspinner';
import { ErrorBanner } from './components/errorbanner';
import { ServerErrorMessage } from './components/servererror';
import { SquareGrid } from './components/squaregrid';
import { useSquares } from './useHooks/useSquares';

export default function Home() {
  const {
    squares,
    isLoaded,
    newSquareId,
    error,
    setError,
    fetchSquares,
    addSquare,
    clearSquares
  } = useSquares()
   
  if (!isLoaded) {
    return (
      <LoadingSpinner />
    );
  }
  
  return (
    <div className={`${styles.container}`}>
      {error.isError && <ErrorBanner error={error} onClose={() => setError({ isError: false, message: '', statusCode: 0 })} />}
      
      <header className={`${styles.header}`}>
        <Image  
          src="/assets/wizardworks-logo-white.png"
          width={200}
          height={200}
          alt="WizardWorks Logo" 
        />
      </header>      

      <div className={`${styles.btnGroup}`}>
        <button 
          onClick={addSquare}
          className={`${styles.btn} ${styles.btnYellow}`}
          disabled={error.isError && error.statusCode >= 500}
        >
          Add square
        </button>
        <button 
          onClick={clearSquares}
          className={`${styles.btn} ${styles.btnPink}`}
          disabled={(error.isError && error.statusCode >= 500) || squares.length === 0}
        >
          Clear
        </button>
        {error.isError && (
          <button
            onClick={fetchSquares}
            className={`${styles.btn} ${styles.btnBlue}`}
          >
            Retry Connection
          </button>
        )}
      </div>
      
      <SquareGrid squares={squares} newSquareId={newSquareId}/>
      
      {error.isError && error.statusCode >= 500 && <ServerErrorMessage statusCode={error.statusCode} />}
      
    </div>
  );
}