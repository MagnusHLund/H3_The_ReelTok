import Router from './Router';
import { StatusBar } from "expo-status-bar";
import React from "react";
import { StyleSheet, Text, View } from "react-native";
import CustomDropdown from "./components/input/CustomDropdown";
import ImagePickerExample from "./components/Layout/common/MediaSelector";
import CameraSelected from "./components/Layout/common/CameraSelected";

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
      <StatusBar style="auto" />
      
    
      <CameraSelected/>
      {/* <CustomDropdown label="Category" categories={Categories} placeholder="Categori"/> */}

      {/* <ImagePickerExample/> */}

    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
    maxWidth: "80%",
    maxHeight: "80%",
  },
});
