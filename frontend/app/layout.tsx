import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "Squaer Overflow",
  description: "Grid chaos initialized",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        {children}
      </body>
    </html>
  );
}
