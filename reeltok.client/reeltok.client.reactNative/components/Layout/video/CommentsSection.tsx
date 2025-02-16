import React, { useState } from 'react';
import { Modal, View, Text, StyleSheet } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import CustomButton from '../../input/CustomButton';

interface CommentSectionProps {
  comments?: React.ReactNode,
  commentsAmount?: number,
  showComments: boolean,
  onClose: () => void,
}

const CommentSection: React.FC<CommentSectionProps> = ({comments, commentsAmount, showComments, onClose}) => {
  
  const styles = StyleSheet.create({
      modalOverlay: {
        display: 'flex',
        justifyContent: 'flex-end',
        flexDirection: 'column',
        backgroundColor: 'transparent',
        height: '100%',
      },
      modalContent: {
        backgroundColor: '#262626',
        padding: 20,
        borderTopLeftRadius: 15,
        borderTopRightRadius: 15,
        height: '50%',
        overflow: 'scroll',
      },
      commentsDetailsAndControls: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        flexDirection: 'row',
        borderBottomWidth: 1,
        borderColor: 'white',
        color: 'white',
      }
  });  
  
  return (
    <>
      <Modal
        animationType="slide"
        transparent={true}
        visible={showComments}
        onRequestClose={onClose}
      >
        <View style={styles.modalOverlay}> 
          <View style={styles.modalContent}>
            <View style={styles.commentsDetailsAndControls}>
              <Text style={{ color: 'white' }}> Comments: {commentsAmount} </Text>
              <CustomButton transparent={true}  borders={false} onPress={onClose}>
                <Ionicons name="close" size={32} color="white" />
              </CustomButton>
            </View>
            {comments} 
          </View>
        </View>
      </Modal>
    </>
  )
};

export default CommentSection;
