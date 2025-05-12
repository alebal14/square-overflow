export interface Position {
  x: number;
  y: number;
}

export interface Color {
  red: number;
  green: number;
  blue: number;
}

export interface Square {
  position: Position;
  color: Color;
}

export interface ErrorState {
  isError: boolean;
  message: string;
  statusCode: number;
}