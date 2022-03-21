using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Reactive
{
    /// <summary>
    /// Класс для выполнения цепочки действий при изменении свойств 
    /// </summary>
    /// <typeparam name="T">Потомок класса DependencyObject</typeparam>
    public class ReactiveFromDependencyProperty<T> : ReactiveBase<T>, IReactive<T>, IDisposable where T : DependencyObject
    {
        private List<DependencyPropertyDescriptor> _descriptors = new List<DependencyPropertyDescriptor>();
        private T _sourse;

        public ReactiveFromDependencyProperty(T sourse, params DependencyProperty[] property)
        {
            _sourse = sourse;
            foreach (var i in property) //Подписываемся
            {
                DependencyPropertyDescriptor d = DependencyPropertyDescriptor.FromProperty(i, typeof(T));
                d.AddValueChanged(sourse, OnPropertyChanged);
                _descriptors.Add(d);
            }
        }

        private void OnPropertyChanged(object sender, EventArgs e) => Invoke(_sourse);

        public override void Dispose() => _descriptors.ForEach(x => x.RemoveValueChanged(_sourse, OnPropertyChanged));
    }
}