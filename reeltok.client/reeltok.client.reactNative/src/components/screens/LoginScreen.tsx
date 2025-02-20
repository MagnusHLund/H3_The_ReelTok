import { View, StyleSheet, Image } from "react-native";
import React from "react";
import CustomTextInput from "../input/CustomTextInput";
import CustomButton from "../input/CustomButton";
import { useWindowDimensions } from "react-native";

const LoginScreen = () => {

  const {height, width} = useWindowDimensions();
  const styles = StyleSheet.create({
    container:{
      height: height,
      width: width,

    },
    inputcontainer: {
      flexDirection: "column",
      marginLeft: "10%",
      top: "20%",
      justifyContent: "space-evenly",
      height: "30%",
      marginBottom: '-30%',
      maxHeight: '50%',
    },
    logocontainer: {
      display: "flex",
    },
    logo: {
      justifyContent: "center",
      width: "90%",
      height: "70%",
    },
  });
  return (
    <View styles={styles.container}>
      <View style={styles.logocontainer}>
        <Image
          style={styles.logo}
          source={require("./../../../assets/images/icons/ReelTok_3.png")}
        />
      </View>
      <View style={styles.inputcontainer}>
        <CustomTextInput placeholder="Email.." email></CustomTextInput>
        <CustomTextInput placeholder="password.." password></CustomTextInput>
        <CustomButton
          widthPercentage={0.76}
          onPress={() => console.log("submitited")}
          title="Log ind"
        ></CustomButton>
      </View>
    </View>
  );
};

export default LoginScreen;
