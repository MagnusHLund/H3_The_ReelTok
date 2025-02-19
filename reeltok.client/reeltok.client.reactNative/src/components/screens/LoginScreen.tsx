import { View, StyleSheet, Image } from "react-native";
import React from "react";
import CustomTextInput from "../input/CustomTextInput";
import CustomButton from "../input/CustomButton";

const LoginScreen = () => {
  const styles = StyleSheet.create({
    container: {
      flexDirection: "column",
      marginLeft: "10%",
      top: "20%",
      justifyContent: "space-evenly",
      height: "30%",
    },
    logocontainer: {
      display: "flex",
    },
    logo: {
      justifyContent: "center",
      width: "90%",
      height: "59%",
    },
  });
  return (
    <View>
      <View style={styles.logocontainer}>
        <Image
          style={styles.logo}
          source={require("./../../../assets/ReelTok_3.png")}
        />
      </View>
      <View style={styles.container}>
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
