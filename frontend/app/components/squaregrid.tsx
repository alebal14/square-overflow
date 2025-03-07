import React from 'react';
import { Square } from '../interfaces';
import styles from '../styles/squarepage.module.css';

interface SquareGridProps {
  squares: Square[];
  newSquareId: string;
}

export function SquareGrid({ squares, newSquareId }: SquareGridProps) {
  let width = 1, height = 1;
  if (squares.length > 0) {
    width = Math.max(...squares.map(s => s.position.y)) + 1;
    height = Math.max(...squares.map(s => s.position.x)) + 1;
  }

  return (
    <div className={`${styles.squareGroup}`} style={{        
      gridTemplateColumns: `repeat(${width}, 60px)`,
      gridTemplateRows: `repeat(${height}, 60px)`,
    }}>
      {squares.map((square) => (
        <div
          key={`${square.position.x}-${square.position.y}`}
          className={`
            ${newSquareId === `${square.position.x}-${square.position.y}` ? `${styles.fadeIn}` : `${styles.square}`}
          `}
          style={{              
            backgroundColor: `rgb(${square.color.red}, ${square.color.green}, ${square.color.blue})`,              
            gridColumn: square.position.y+1,
            gridRow: square.position.x+1
          }}
        />
      ))}
    </div>
  );
}