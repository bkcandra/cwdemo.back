import React from 'react';

interface MainTitleProps {
  title: string;
  setTitle: React.Dispatch<React.SetStateAction<string>>;
}

export const MainTitle = React.createContext<MainTitleProps>({
  title: '',
  setTitle: () => {},
});