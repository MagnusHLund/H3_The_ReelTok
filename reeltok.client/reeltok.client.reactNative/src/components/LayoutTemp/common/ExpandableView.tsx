import { Animated, StyleSheet } from 'react-native'
import React, { ReactNode, useEffect, useState } from 'react'

interface ExpandableViewProps {
  expanded: boolean
  children?: ReactNode
}

const ExpandableView: React.FC<ExpandableViewProps> = ({ expanded, children }) => {
  const [height] = useState(new Animated.Value(0))

  useEffect(() => {
    Animated.timing(height, {
      toValue: expanded ? 150 : 0,
      duration: 150,
      useNativeDriver: false,
    }).start()
  }, [expanded])

  return (
    <Animated.View style={[styles.viewContainer, { maxHeight: height }]}>{children}</Animated.View>
  )
}

const styles = StyleSheet.create({
  viewContainer: {
    backgroundColor: 'white',
    width: '100%',
    bottom: 27.5,
    height: 200,
    position: 'absolute',
  },
})

export default ExpandableView
