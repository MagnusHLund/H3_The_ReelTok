import Router from './Router';
import React from 'react';
import { StatusBar } from "expo-status-bar";
import React from "react";
import { StyleSheet, Text, View } from "react-native";
import CustomDropdown from "./components/input/CustomDropdown";

const Categories = [
  { label: 'Gaming', value: 'Gaming' },
  { label: 'Tech', value: 'Tech' },
  { label: 'Dance', value: 'Dance' },
  { label: 'Fight', value: 'Fight' },
  { label: 'Sport', value: 'Sport' },
  { label: 'Comedy', value: 'Comedy' },
];

export default function App() {
  return (
    <View style={styles.container}>
      <Text>Open up App.tsx to start working on your app!</Text>
      <StatusBar style="auto" />
      
    
      <CustomDropdown label="Category" categories={Categories} placeholder="Categori"/>
    </View>
  );
}
