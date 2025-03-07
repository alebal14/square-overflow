import { useState, useEffect } from 'react';
import { ErrorState, Square } from '../interfaces';

export function useSquares() {
  const [squares, setSquares] = useState<Square[]>([]);
  const [isLoaded, setIsLoaded] = useState<boolean>(false);
  const [newSquareId, setNewSquareId] = useState<string>('');
  const baseUrl = "https://localhost:7194/api/Square";
  const [error, setError] = useState<ErrorState>({
    isError: false,
    message: '',
    statusCode: 0
  });
  
  useEffect(() => {
    fetchSquares();
  }, []);
  
  const handleApiError = (error: any) => {
    console.error('API Error:', error);
    const statusCode = error.message.includes(':') ? parseInt(error.message.split(':')[0]) : 0;
    setError({
      isError: true,
      message: error.message || 'API request failed',
      statusCode
    });
  };

  const resetError = () => {
    setError({ isError: false, message: '', statusCode: 0 });
  };
  
  const fetchSquares = async () => {

    if (error.isError) {
        resetError();
    }

    try {
      const response = await fetch(baseUrl);
      
      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(`${response.status}: ${errorMessage || response.statusText}`);
      }
      
      const data = await response.json();
      setSquares(data);
      setIsLoaded(true);

    } catch (error: any) {
      handleApiError(error);
      setIsLoaded(true);
    }
  };
  
  const addSquare = async () => {

    if (error.isError) {
      resetError();
    }

    try {
      const response = await fetch(baseUrl, { method: 'POST' });
      
      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(`${response.status}: ${errorMessage || response.statusText}`);
      }
      
      const newItem = await response.json();

      setSquares(prevItems => [...prevItems, newItem]);      
      setNewSquareId(`${newItem.position.x}-${newItem.position.y}`);

      setTimeout(() => setNewSquareId(''), 3000);

    } catch (error: any) {
      handleApiError(error);
    }
  };
  

  const clearSquares = async () => {

    if (error.isError) {
      resetError();
    }
    
    try {
      const response = await fetch(baseUrl, { method: 'DELETE' });
      
      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(`${response.status}: ${errorMessage || response.statusText}`);
      }
      
      setSquares([]);
    } catch (error: any) {
      handleApiError(error);
    }
  };
  

  return {
    squares,
    isLoaded,
    newSquareId,
    error,
    setError,    
    fetchSquares,
    addSquare,
    clearSquares
  };
}