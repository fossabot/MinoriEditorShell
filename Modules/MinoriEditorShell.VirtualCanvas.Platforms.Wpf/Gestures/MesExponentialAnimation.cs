//-----------------------------------------------------------------------
// <copyright file="ExponentialAnimation.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Gestures
{
    /// <summary>
    /// EdgeBehavior defines what type of exponential animation to do.
    /// </summary>
    public enum EdgeBehavior {
        EaseIn, EaseOut, EaseInOut
    }

    /// <summary>
    /// This class provides a non-linear DoubleAnimation.
    /// </summary>
    public class MesExponentialDoubleAnimation : DoubleAnimation {

        /// <summary>
        /// The property for defining an EdgeBehavior value.
        /// </summary>
        public static readonly DependencyProperty EdgeBehaviorProperty =
            DependencyProperty.Register("EdgeBehavior", typeof(EdgeBehavior), typeof(MesExponentialDoubleAnimation), new PropertyMetadata(EdgeBehavior.EaseIn));

        /// <summary>
        /// Property for defining the exponential power of the animation.
        /// </summary>
        public static readonly DependencyProperty PowerProperty =
            DependencyProperty.Register("Power", typeof(Double), typeof(MesExponentialDoubleAnimation), new PropertyMetadata(2.0));

        /// <summary>
        /// Construct new empty ExponentialDoubleAnimation object.
        /// </summary>
        public MesExponentialDoubleAnimation() {
        }

        /// <summary>
        /// Construct new ExponentialDoubleAnimation with given arguments
        /// </summary>
        /// <param name="from">Animate from this value</param>
        /// <param name="to">To this value</param>
        /// <param name="power">With this exponential power</param>
        /// <param name="behavior">Using this type of behavior</param>
        /// <param name="duration">For this long</param>
        public MesExponentialDoubleAnimation(Double from, Double to, Double power, EdgeBehavior behavior, Duration duration) {
            EdgeBehavior = behavior;
            Duration = duration;
            Power = power;
            From = from;
            To = to;
        }

        /// <summary>
        /// Get/Set the exponential behavior.  It can either start fast and finish slow (EaseIn), or it can start slow
        /// and finish fast (EaseOut) or it can do both (like a parabola) which is EaseInOut. Default is EaseIn.
        /// </summary>
        public EdgeBehavior EdgeBehavior
        {
            get => (EdgeBehavior)GetValue(EdgeBehaviorProperty);
            set => SetValue(EdgeBehaviorProperty, value);
        }

        /// <summary>
        /// Get/Set the power of the exponential.  The default is 2.
        /// </summary>
        public Double Power
        {
            get => (Double)GetValue(PowerProperty);
            set
            {
                if (value > 0.0)
                {
                    SetValue(PowerProperty, value);
                }
                else
                {
                    throw new ArgumentException("cannot set power to less than 0.0. Value: " + value);
                }
            }
        }

        /// <summary>
        /// This method is called by WPF to implement the actual animation, so this method calculates 
        /// the exponential value based on how long we've been running so far.
        /// </summary>
        /// <param name="defaultOriginValue"></param>
        /// <param name="defaultDestinationValue"></param>
        /// <param name="clock"></param>
        /// <returns></returns>
        protected override Double GetCurrentValueCore(Double defaultOriginValue, Double defaultDestinationValue, AnimationClock animationClock) {
            Double returnValue;
            Double start = (Double)From;
            Double delta = (Double)To - start;
            Double timeFraction = animationClock.CurrentProgress.Value;
            if (timeFraction == 1)
            {
                return (Double)To;
            }
            switch (EdgeBehavior) {
                case EdgeBehavior.EaseIn:
                    returnValue = EaseIn(timeFraction, start, delta, Power);
                    break;
                case EdgeBehavior.EaseOut:
                    returnValue = EaseOut(timeFraction, start, delta, Power);
                    break;
                case EdgeBehavior.EaseInOut:
                default:
                    returnValue = EaseInOut(timeFraction, start, delta, Power);
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// All Freesable objects have to implement this method.
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore() => new MesExponentialDoubleAnimation();

        /// <summary>
        /// Impelement the EaseIn style of exponential animation which is one of exponential growth.
        /// </summary>
        /// <param name="timeFraction">Time we've been running from 0 to 1.</param>
        /// <param name="start">Start value</param>
        /// <param name="delta">Delta between start value and the end value we want</param>
        /// <param name="power">The rate of exponental growth</param>
        /// <returns></returns>
        private static Double EaseIn(Double timeFraction, Double start, Double delta, Double power) {
            // math magic: simple exponential growth
            Double returnValue = Math.Pow(timeFraction, power);
            returnValue *= delta;
            returnValue += start;
            return returnValue;
        }

        /// <summary>
        /// Impelement the EaseOut style of exponential animation which is one of exponential decay.
        /// </summary>
        /// <param name="timeFraction">Time we've been running from 0 to 1.</param>
        /// <param name="start">Start value</param>
        /// <param name="delta">Delta between start value and the end value we want</param>
        /// <param name="power">The rate of exponental decay</param>
        /// <returns></returns>
        private static Double EaseOut(Double timeFraction, Double start, Double delta, Double power)
        {
            // math magic: simple exponential decay
            Double returnValue = Math.Pow(timeFraction, 1 / power);
            returnValue *= delta;
            returnValue += start;
            return returnValue;
        }

        /// <summary>
        /// Impelement the EaseInOut style of exponential animation which is one of exponential growth
        /// for the first half of the animation and one of exponential decay for the second half.
        /// </summary>
        /// <param name="timeFraction">Time we've been running from 0 to 1.</param>
        /// <param name="start">Start value</param>
        /// <param name="delta">Delta between start value and the end value we want</param>
        /// <param name="power">The rate of exponental growth/decay</param>
        /// <returns></returns>
        private static Double EaseInOut(Double timeFraction, Double start, Double delta, Double power)
        {
            Double returnValue;

            // we cut each effect in half by multiplying the time fraction by two and halving the distance.
            if (timeFraction <= 0.5) {
                returnValue = EaseOut(timeFraction * 2, start, delta / 2, power);
            } else {
                returnValue = EaseIn((timeFraction - 0.5) * 2, start, delta / 2, power);
                returnValue += (delta / 2);
            }
            return returnValue;
        }
    }
}